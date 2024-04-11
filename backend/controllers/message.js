const MessageService = require("../services/message");
const messageService = new MessageService();

async function createUserMessage(req, res) {
  const { storyId } = req.params;
  const { content } = req.body;

  try {
    const message = await messageService.createUserMessage(storyId, content);
    res.json({ message });
  } catch (error) {
    console.error("Error creating user message:", error);
    res
      .status(error.message === "Story not found" ? 404 : 500)
      .json({ error: error.message || "Failed to create user message" });
  }
}

async function createChatbotMessage(req, res) {
  const { storyId } = req.params;

  try {
    const { newMessage, chatOver } = await messageService.createChatbotMessage(
      storyId
    );
    res.json({ message: newMessage, chatOver });
  } catch (error) {
    console.error("Error creating chatbot message:", error);
    res
      .status(error.message === "Story not found" ? 404 : 500)
      .json({ error: error.message || "Failed to create chatbot message" });
  }
}

module.exports = {
  createUserMessage,
  createChatbotMessage,
};
