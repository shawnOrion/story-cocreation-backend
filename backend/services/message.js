const Story = require("../models/story");
const Message = require("../models/message");
const OpenAIService = require("./openai");
let { chatInstructions, endChatPhrase } = require("../instructions/chat");

class MessageService {
  async createUserMessage(storyId, content) {
    const story = await Story.findById(storyId);
    if (!story) {
      throw new Error("Story not found");
    }

    const message = new Message({
      content,
      role: "user",
    });
    await message.save();
    story.messages.push(message._id);
    await story.save();

    return message;
  }

  async createChatbotMessage(storyId) {
    const story = await Story.findById(storyId);
    if (!story) {
      throw new Error("Story not found");
    }

    const instructions = chatInstructions.replace(
      "{READING_LEVEL}",
      story.readingLevel
    );
    const systemMsg = OpenAIService.FormatMessage("system", instructions);
    const messages = [systemMsg];

    for (const messageId of story.messages) {
      const message = await Message.findById(messageId);
      const formattedMessage = OpenAIService.FormatMessage(
        message.role,
        message.content
      );
      messages.push(formattedMessage);
    }

    const content = await OpenAIService.GetResponse(messages);
    const newMessage = new Message({
      role: "assistant",
      content,
    });
    await newMessage.save();
    story.messages.push(newMessage._id);
    await story.save();

    const chatOver = newMessage.content.includes(endChatPhrase);

    return { newMessage, chatOver };
  }
}

module.exports = MessageService;
