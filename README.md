# DTRealestate - Realestate agent management application

This application is a simple, intuitive application mainly tailored towards realestate agents and the administrators overseeing their activity. It allows agents to add new realestates to the system, add new offers for each existing realestate, add and view completed contracts, view and reply to user inquiries. It also allows administrators to activate or deactivate agent accounts.

***
# Available functionalities

### Registration

Agents can create a new account. This account must be activated by an administrator before the agent can use the application. The registraction screen is shown on the image below.
![signup](https://github.com/Chameleeon/DTRealestate/blob/main/Screenshots/signup.png)

### Agent activation

As mentioned above, agents cannot log in to their account until it has been activated. The administrator view for activating and deactivating agent accounts is shown below.
![activate](https://github.com/Chameleeon/DTRealestate/blob/main/Screenshots/admin_agents.png)

### Log in

After the agent account has been activated, the agent can loog in. The log in screen is a standard, simple log in screen taking in a username and a password, checking the password hash to the one stored in the database and if they match, the agent is logged in and can perform various actions.

![login](https://github.com/Chameleeon/DTRealestate/blob/main/Screenshots/login.png)

## Agent functionalities

All possible agent functionalities implemented in the application are shown below, along with instructions on how to use them.

### Agent dashboard

Logging in opens the default dashboard view, which can also be accessed by clicking the **Home** button in the navigation menu. The dashboard shows some basic information for the agent, such as the number of inquiries he has, number of completed contracts and number of active offers.
![dashboard](https://github.com/Chameleeon/DTRealestate/blob/main/Screenshots/home_agent.png)
