// controllers/openai.js
require("dotenv").config();
const { OpenAI } = require("openai");

class OpenAIService {
  constructor(apiKey) {
    this.openai = new OpenAI({ apiKey });
  }

  FormatMessage(role, content) {
    return {
      role: role,
      content: content,
    };
  }

  async GetResponse(messages) {
    const completion = await this.openai.chat.completions.create({
      messages: messages,
      model: "gpt-4-1106-preview",
    });
    return completion.choices[0].message.content;
  }
}

module.exports = new OpenAIService(process.env.OPENAI_API_KEY);
