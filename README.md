# Feeder exercise

The Feeder application displays posts and associated comments retrieved from a database component. It is currently incomplete: it has a startup screen and a main screen that displays a list of post titles, but does not show any detail about the posts and post comments.

The aim of this exercise is to complete the application by completing the [Tasks](#tasks) listed below.

See [Provided components](#provided-components) for some information that may assist with these tasks.

## Submission

Your code should be available on a GIT repository of your choosing ([bitbucket](https://bitbucket.org), [GitHub](https://github.com/), etc). Once you are satisfied with your code, send us an invitation to access the code so we can take a look.

Make sure your final submission includes all the code and libraries required to compile and run the application and tests. Make sure all tests pass.

## Tasks

Here are the tasks required to complete the application. It is advisable to commit to your git repository after each task (or even multiple times per task). You are welcome to change any of the provided code to help you achieve these tasks.

### Task 1: Improve application responsiveness and reliability

The [provided `Database` component](#database) responds slowly and sometimes times-out. This gives a poor experience to users, and also makes the application difficult to test. The source code for this component is not included, so you will need to write your own wrapper around the database calls and adapt the application code to use that wrapper in order to implement the following features:

* the application should show an error message if the database times-out. An `ErrorView.xaml` screen and related navigation code is already provided.
* the `NavigatorFixture.GoingToMain` test should be implemented and pass.
* the application should show a loading indicator while waiting for the database response. One possible design is shown below, but feel free to use a different design:

![Proposed loading screen design](./samples/5.LoadingIndicator.png){ width=300px }

The application should stay responsive when loading (for example: can be resized and still redraw).


### Task 2: Add post Details screen

* When the user selects a post on the Main screen, the application should navigate to a new screen that displays post details. A sample design is show below. You do not have to match this exactly, but your implementation should include all the information from the sample: post title and body from `Post` object, author name and image from `User` object, comments and commenter names from `Comment` object (this can all be retrieved from the `Database` component).
* The user should be able to go back to the Main screen. If the database returns new posts since the last visit to this screen then these should be included on the Main screen.
* Provide automated tests for navigating to and from this screen, as well as for the information to be displayed on the screen when it loads (this can be via an associated view model, you do not need automated tests for the XAML).

![Proposed detail screen design](./samples/3.PostDetails.png){ width=300px }

## Provided components

The following components are provided as a starting point for this exercise. It is fine to change any of the included code, although you are stuck having to use the provided `Database` component, and if you invalidate the provided tests you should reimplement them to maintain the same test coverage.

### Existing screens

![Intro screen](./samples/0.Intro.png){ width=25% }\ 
![Main screen](./samples/2.FilteredPosts.png){ width=25% }\ 
![Error screen](./samples/4.ErrorMessage.png){ width=25% }

### Database

The provided `Database` component simulates an unreliable connection to a data source. It has the following properties:

* It uses slow, blocking calls and will sometimes timeout and throw an exception.
* It will respond with different data each time the application is loaded.
* Repeated calls to `GetPostSummaries` and `GetComments` can return new posts and comments, to simulate the database receiving new information while the application is running.
* No data will ever be deleted from the database while the application is running.

You cannot change this component, but you can wrap it with a new class from within the application code. 

Two connection strings are provided to aid with manually testing the application, one for Default reliability, the other to simulate an even less reliable connection (useful for checking error handling).

### Application shell and navigator

The basic shell of the application is provided. An `AppShellViewModel` is bound to the application window on startup and is used to host screens. A screen consists of a view model class and a XAML view. These two pieces are linked together for each screen using data templates defined in `ViewMappings.xaml`. The introduction screen and main screen are provided as examples of how views integrate with the shell. The navigator component is currently used to transition between views (although you can change this if you want to).

### Application styles

The solution includes a file containing all the XAML styles currently used on the views. You can add, edit, or ignore styles as needed.

### Tests

Some sample tests are provided using MSTest and the [NSubstitute](http://nsubstitute.github.io/) mocking library. (You can switch to other testing frameworks if you prefer, but make sure you keep the existing test coverage.) A few are marked `Inconclusive` as they can not be implemented until some steps in the exercise have been completed.

## Tips

* Provide meaningful commit messages to help us follow your decision-making process. You can also document some of the decisions you made within code comments.
* Follow the current code conventions.
* Take care to provide helpful member names and/or comments.
* If you spot duplication make sure you extract it into a common method or a new class. (Or provide a comment explaining why you left it in.)
* If you don't feel a test is necessary for a particular piece of code, it is a good idea to include a brief comment as to why you are not testing that class or member.

