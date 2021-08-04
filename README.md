<h1>Read Later</h1>
<h2>A revolution in the world of social bookmarking</h2>
Read Later is a fictional service designed to test a range of skills using the .net core 5 MVC architecture. For full details about this project, the libraries used, and a quickstart guide to getting it up and running, please visit the About page

<h3>Tasks</h3>
<p>Please complete 3 of the below exercises.  You will need to complete the first 2, and then choose 1 from the remaining 3 however feel free to complete more than 3, or to elaborate on the ways you achieve the solution.</p>
<p>
    This test is designed to measure your coding knowledge and skill however as time is limited you are not expected to produce 100% production ready code in all cases.  Most important is that the code works, and has the correct logical process
    and structure to solve the problem.  Coding standards e.g. error logging and reporting, correct variable naming, conde commenting, unit testing are not required - you won't lose marks for forgetting to check a null reference here and there!
</p>
<h4>Complete these 2 first:</h4>
<ol>
    <li>
        <h5>Bookmark management</h5>
        Implement full CRUD management for Bookmarks.  Users should be able to create a new category whilst creating a bookmark without requiring any page refresh
    </li>    
    <li>
        <h3>Bookmark management - Checked</h3>        
        I've implemented full CRUD management for Bookmarks (Added a Bookmark Controller with all the methods). I've added an option to create a Category while creating a Bookmark with jquery.
    </li>    
    <li>
        <h5>User accounts</h5>
        The package has the default AspNetCore Identity installed however not implemented fully.  Complete this implementation and change the entities to work on a per user basis.  For additional credit,
        implement multiple membership providers allowing users to log in with OpenID services
    </li>    
    <li>
        <h3>User acoounts - Checked</h3>        
        I've added a mail service (in Service Project) that helps the implementation of Identity. I've added [Authorize] data annotation on each controller, so only logged-in users can work with bookmarks and categories. Added log-in option with Google mail and Facebook user.
    </li>
</ol>
<h4>Now choose one of these:</h4>
<ol>
    <li>
        <h5>API access</h5>
        Expose an API allowing external systems to manage bookmarks.  You will need to consider authentication / access tokens
    </li>    
     <li>
        <h3>API access - Checked</h3>        
        I've created 2 API controllers (with methods for managing bookmarks and categories). I've added JWT token authentication. Swagger is also added, for easier understanding of the API's.
    </li>
    <li>
        <h5>Tracking and reporting</h5>
        Track each time a user clicks out on one of their saved bookmarks and provide a simple dashboard which can show a summary of stats by user, and as an overview (e.g. for tracking the most popular links).
        Users should also be able to share a short url with their friends which when clicked would also log usage statictics and be reported on
    </li>
    <li>
        <h3>Tracking and reporting - Checked</h3>        
        I don't know if I fully understand this part, so I've created a new table in the database for storing clicks, also I've created a dashboard that shows the all-time most clicked links, and the most clicked links for today, together with a Facebook share button.
    </li>
    <li class="three">
        <h5>Website widget</h5>
        Provide 1 or more widgets that can be used in an external website, regardless of the server side technology.  These can provide any functionality you choose - for example showing the most recent 5 bookmarks for a particular user, or the 3 most popular bookmarks today
    </li>    
    <li>
        <h3>Website widget - Checked</h3>        
        I've created iframe widget that is showing the top 5 bookmarks for today. (Currently is implemented on _Layout.cshtml so is available on every view in the application)
    </li>
</ol>
