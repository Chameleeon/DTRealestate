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

### Realestates

This page allows agents to view existing realestates in the database, edit them and add new ones. Only administrators can delete realestates from the database. These realestates can be searched, filtered and sorted by different criteria.
![realestates](https://github.com/Chameleeon/DTRealestate/blob/main/Screenshots/realestates.png)

Clicking the **Edit** button in he top right of the realestate card opens the editing popup for the given realestate, allowing the agent to modify any of the existing values.</br>
![edit](https://github.com/Chameleeon/DTRealestate/blob/main/Screenshots/edit_realestate.png)

Clicking the **Info** button in the bottom right allows the agent to view detailed info about the given realestate.
![edit](https://github.com/Chameleeon/DTRealestate/blob/main/Screenshots/realestate_info.png)

Clicking the **Add** button next to the info button allows the agent to add a new offer for the given realestate. Each realestate can have multiple offers.</br>
![add_offer](https://github.com/Chameleeon/DTRealestate/blob/main/Screenshots/add_offer.png)

The button **Add Realestate** allows the agent to add a new realestate by opening a new popup window.
![add_realestate](https://github.com/Chameleeon/DTRealestate/blob/main/Screenshots/add_realestate.png)
