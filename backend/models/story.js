const mongoose = require("mongoose");

const storySchema = new mongoose.Schema({
  readingLevel: String,
  messages: [{ type: mongoose.Schema.Types.ObjectId, ref: "Message" }],
  summary: {
    theme: String,
    character: String,
    setting: String,
  },
  content: {
    title: String,
    text: String,
  },
});

const Story = mongoose.model("Story", storySchema);

module.exports = Story;
