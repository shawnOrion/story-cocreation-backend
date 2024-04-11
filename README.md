# Project Notes
### What does the project do?
This app lets kids chat with an AI to create their own stories. They choose what their story is about. who's in it and where it happens. Then, the AI writes a story for them to read, fit to their reading level.

### Why is it important?
I started this project because I know kids don't often like the books they have to read. This app makes reading fun by giving them a chance to make their own stories. It's a new way to help kids love reading and learning.

### What’s the core functionality?
The app uses GPT to make story creation fun and easy for kids. First, GPT chats with them so they can pick out their story's theme, characters, and setting. Then the chat gets turned into a story summary in JSON format by GPT, to organize all their ideas. Then, GPT uses the summary to write a story matched to the kid's reading level. Each step builds on the last, to make sure the final story is something the kid helped create and will love to read.

### How did I build it?
The backend is built with Node.js and Express, paired with Unity and C# on the frontend. Unity and C# were chosen as technical challenges to help improve my skills in creating interactive AI applications. These choices helped me get better at building an app that real kids can use.

### How is the project structured?
- On the Front-End with Unity and C#, I followed the MVC pattern, where Views take care of the UI, Controllers manage the link between UI and data, and Services oversee communication with the backend. 
- Also, the front-end has an app controller designed to smoothly manage view transitions. It responds to the results of key user actions and dictates which screen will appear next. This setup gives a clear overview of the user's journey through the app.
- The Back-End is powered by Express, Node.js, and Mongoose, organized into Models for the data structure, Routes for guiding API calls, and Controllers plus Services for managing requests and data.

### What was challenging about this project?
Figuring out how to make the front-end as simple and logical as the story creation process wasn’t easy. The main headache was designing the code to clearly show how the user moves through the app. I experimented with new coding strategies, and refactored the app with an app controller and event-driven communication. This helped make the app a lot easier to understand at a glance.

### What did I learn?
This project was personal. I was constantly flipping between developer and user modes. This perspective pushed me to refine the app until it met both my technical standards, and also genuinely solved the problem at hand. I realized how much I love solving real issues through tech, especially when I see a chance for a better way of doing things. 
