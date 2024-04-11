const StoryService = require("../services/story");
const storyService = new StoryService();

const createStory = async (req, res) => {
  try {
    const { messages } = req.body;
    const newStory = await storyService.createStory(messages);
    res.json({ story: newStory });
  } catch (error) {
    console.error(error);
    res.status(500).json({ error: "Internal Server Error" });
  }
};

const updateSummary = async (req, res) => {
  try {
    const { storyId } = req.params;
    const updatedStory = await storyService.updateSummary(storyId);
    res.json({ story: updatedStory });
  } catch (error) {
    console.error(error);
    res.status(500).json({ error: "Internal Server Error" });
  }
};

const updateContent = async (req, res) => {
  try {
    const { storyId } = req.params;
    const updatedStory = await storyService.updateContent(storyId);
    res.json({ story: updatedStory });
  } catch (error) {
    console.error(error);
    res.status(500).json({ error: "Internal Server Error" });
  }
};

const updateReadingLevel = async (req, res) => {
  try {
    const { storyId } = req.params;
    const { readingLevel } = req.body;
    const updatedStory = await storyService.updateReadingLevel(
      storyId,
      readingLevel
    );
    res.json({ story: updatedStory });
  } catch (error) {
    console.error(error);
    res.status(500).json({ error: "Internal Server Error" });
  }
};

module.exports = {
  createStory,
  updateReadingLevel,
  updateContent,
  updateSummary,
};
