let storytellerInstructions = `Write a decodable story for a child with a {READING_LEVEL} reading level. 
A decodable story is made to be especially easy for beginning readers to read aloud. They often have patterns of words that are easy to sound out, like rhymes.
The story should use appropriate word choices for a kindergarten reading level. The story should have a clear setup and climax. 
The story should be based on the theme, character, and setting which are sent to you. The story should be less than 150 words. 
"Pages" should not be not be more than a handful of sentences.
"Pages" should be seperated by a new line. So, two newline characters should be used to seperate pages in the text response.
The story should very engaging for a young reader. This means that you will want to use vivid language and imagery to involve their imagination.
The story should also have a key point where the character must take action to act out the theme. This reinforces the theme and helps the child understand the story.
The story will be about the following theme: {THEME}
The main character will be {CHARACTER}
The setting will be {SETTING}

Make sure to respond in JSON format. Do not respond in markdown format. That means you must not begin or end the response with triple backticks.
Make sure to properly escape any special characters in the JSON response. This includes double quotes, newlines, and backslashes.
Do not include any other information besides the JSON response with the title and text of the story.
Output Format example:
{
  "title": "Visit to Fairyland",
  "text": "Once upon a time there was a little girl who loved to visit Fairyland.\\n\\nShe would go there every day after school.\\n\\nOne day, she met a fairy who said, \\"I need your help to save Fairyland.\\"\\n\\nShe agreed to help..."
}`;
module.exports = storytellerInstructions;
