var express = require("express");
var router = express.Router();
const {
  createStory,
  updateReadingLevel,
  updateContent,
  updateSummary,
} = require("../controllers/story");
const {
  createUserMessage,
  createChatbotMessage,
} = require("../controllers/message");

router.post("/story", createStory);
router.put("/story/:storyId/reading-level", updateReadingLevel);
router.post("/story/:storyId/user-message", createUserMessage);
router.post("/story/:storyId/chatbot-message", createChatbotMessage);
router.put("/story/:storyId/summary", updateSummary);
router.put("/story/:storyId/content", updateContent);

module.exports = router;
