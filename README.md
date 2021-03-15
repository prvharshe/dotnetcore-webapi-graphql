# Running locally

#### Pre-requisite - *Docker desktop is installed*

1. Download/clone the repository `git clone https://github.com/prvharshe/dotnetcore-webapi-graphql.git`

2. Open terminal/command prompt and cd to SampleWebAPIApplication folder.

3. Run following 2 commands one after another in the terminal -
   
   `docker-compose build`
   
   `docker-compose up`
   
   Which will run the docker-compose.yaml file and Dockerfile which in turn downloads all the important files needed to run the application.
  
4. Open the browser and open following url - `http://localhost:8080/todoitems/swagger/index.html`
   Which will open the swagger UI and all the API's available.
   
5. You can edit this url and open GraphQL querying UI Banana Cake Pop which again displays the same API's just you can query them in with GraphQL.
   
   Edited url - `http://localhost:8080/graphql`
   
   
-------------------------------------------------------------------------------------------------------------------------------------------------------
   
## Querying in with GraphQL

1.  Get all todo items -
    
    ```
      query{
      allTodoItemsOnly{
      id
      description
      isComplete
    }}
  
    
2. Get specific todo item by id -

   ```
     query{
      todoItemsById(id:1){
      id
      description
      isComplete
    }}
   
3. Create a new todo item -

   ```
    mutation{
    createTodoItem(description: "new item from docker",isComplete: false){
      id,
      description,
      isComplete
    }}
    
  
4. Edit a todo item -

   ```
    mutation{
    editTodoItem(id:1,description:"editing the item from docker",isComplete:true){
      id,
      description,
      isComplete
    }}
  
5. Delete a todo item -


   ```
    mutation{
    deleteTodoItem(id:1){
      id,
      description,
      isComplete
    }}
  
