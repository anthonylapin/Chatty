## Chat application

•	Stack: C#, ASP.NET MVC, SQL Server, JavaScript, Bootstrap

Specifications:
-	Authentication & Authorization with AspNetIdentity.
-	Every user can create a chat room and share its link to other users
-	User’s can join a room by the link provided
-	Rooms, where user exists, are displayed at his personal page.
-	After joining a room there is a chat, where user can communicate with other’s users.
-	User can send, edit & delete his messages
-	User cannot delete not his messages
-	Messages are saved to db after each sending
-	When user joins room all messages been sent before are displayed

Models:
-	User: Id, Email, Username, Password, Rooms, Messages
-	Room: Id, Name, Created, Messages, Users
-	Message: Id, Text, Created, User, Room

Relations:
-	User -> many-to-many -> Room
-	User -> one-To-Many -> Message
-	Room -> one-to-many -> Message
