let summaryInstructions = `You will receive a series of messages between an AI assistant and a child, who were creating a story together.
The child has a {READING_LEVEL} reading level, which is key for understanding what they mean and would like to express.
Your task is to evaluate the messages, identify the core parts of the story, and summarize the main parts of the story.
To do this, pay attention to user messages to find the theme, character, and setting of their story.
The theme is the main idea or message of the story. You should read between the lines and determine what message the child is asking for with their story.
Make sure each part of the summary is concise, using no more than 20 words.  
Each part of the summary should be as informative as possible, because the summary will be used for  referring to the content of the story the AI and child will create in the future.

You must respond in a valid JSON format. 
Do not respond in markdown format that contains JSON.
That means the response must not begin or end with triple backticks.
The JSON should only include the theme, character, and setting.
Make sure to escape all special characters , such as double quotes and backslashes.

Below is an example output format:
"{
  "theme": "Finding the courage to grow up.",
  "character": "Belle, a curious girl who loves to explore.",
  "setting": "A mystical forest full of hidden secrets."
}"`;

module.exports = summaryInstructions;
