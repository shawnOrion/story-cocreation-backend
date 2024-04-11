const Story = require("../models/story");
const Message = require("../models/message");
const OpenAIService = require("./openai");
let storytellerInstructions = require("../instructions/storyteller");
let summaryInstructions = require("../instructions/summary");

class StoryService {
  async createStory(messages) {
    let newStory;

    if (messages && messages.length) {
      // switch message ids to actual messages
      const newMessages = await Promise.all(
        messages.map(async (message) => {
          const newMessage = new Message({
            role: message.role,
            content: message.content,
          });
          await newMessage.save();
          return newMessage._id;
        })
      );
      console.log("New Messages:", newMessages);
      newStory = new Story({ messages: newMessages });
    } else {
      newStory = new Story();
    }

    await newStory.save();
    console.log("New Story Object:", newStory);
    return newStory;
  }

  async updateSummary(storyId) {
    const story = await Story.findById(storyId).populate("messages");

    const chatMessagesStr = story.messages
      .map((message) => `${message.role}: ${message.content}`)
      .join("\n");

    const instructions = summaryInstructions.replace(
      "{READING_LEVEL}",
      story.readingLevel
    );

    const summaryMessages = [
      // GPT context
      OpenAIService.FormatMessage("system", instructions),
      OpenAIService.FormatMessage("user", chatMessagesStr),
    ];
    let summary = await OpenAIService.GetResponse(summaryMessages); // responds in JSON format
    const { theme, character, setting } = JSON.parse(summary);
    story.summary = { theme, character, setting };
    const updatedStory = await story.save();
    console.log("Updated Story Object:", updatedStory);
    return updatedStory;
  }

  async updateContent(storyId) {
    const story = await Story.findById(storyId);

    const instructions = storytellerInstructions
      .replace("{READING_LEVEL}", story.readingLevel)
      .replace("{THEME}", story.summary.theme)
      .replace("{CHARACTER}", story.summary.character)
      .replace("{SETTING}", story.summary.setting);

    const messages = [OpenAIService.FormatMessage("system", instructions)]; // GPT context
    let content = await OpenAIService.GetResponse(messages); // responds in JSON format
    const { title, text } = JSON.parse(content);
    story.content = { title, text };
    const updatedStory = await story.save();
    console.log("Updated Story object:", updatedStory);
    return updatedStory;
  }

  async updateReadingLevel(storyId, readingLevel) {
    const story = await Story.findById(storyId);
    story.readingLevel = readingLevel;
    const updatedStory = await story.save();
    return updatedStory;
  }
}

module.exports = StoryService;
