# DTRealestate - Realestate agent management application

This application is a simple, intuitive application mainly tailored towards realestate agents and the administrators overseeing their activity. It allows agents to add new realestates to the system, add new offers for each existing realestate, add and view completed contracts, view and reply to user inquiries. It also allows administrators to activate or deactivate agent accounts.

***
# Available functionalities

Below are listed all of the available functionalities of the application, divided into three categories:
- General
- Administrator
- Agent
General functionalities represent functionalities which are not specific to any user, while the administrator and agent functionalities represent functionalities for that specific type of user.

## General functionalitites

### Registration

Agents can create a new account. This account must be activated by an administrator before the agent can use the application. The registraction screen is shown on the image below.
![signup](https://github.com/Chameleeon/DTRealestate/blob/main/Screenshots/signup.png)

### Log in

After the agent account has been activated, the agent can loog in. The log in screen is a standard, simple log in screen taking in a username and a password, checking the password hash to the one stored in the database and if they match, the agent is logged in and can perform various actions.

![login](https://github.com/Chameleeon/DTRealestate/blob/main/Screenshots/login.png)

### Settings

Clicking on the **Settings** button in the top right corner of the window after the user is logged in will open the **Settings** page, allowing the user to select one of three currently available themes:
- Dark
- Light
- Flame
  
The user can also select one of two currently supported languages:
- English
- Serbian

These settings are saved and are loaded back after the application is closed and opened again, meaning that the user doesn't have to change them every time they open the application.

![settings](https://github.com/Chameleeon/DTRealestate/blob/main/Screenshots/settings.png)

## Administrator functionalities

### Administrator dashboard

The administrator dashboard shows basic info about agents, such as number of activated and deactivated agent accounts.
![admin_dashboard](https://github.com/Chameleeon/DTRealestate/blob/main/Screenshots/admin_dashboard.png)

### Agent activation

As mentioned above, agents cannot log in to their account until it has been activated. The administrator view for activating and deactivating agent accounts is shown below.
![activate](https://github.com/Chameleeon/DTRealestate/blob/main/Screenshots/admin_agents.png)

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

### Contracts

This page allows agents to view completed contracts, search them, sort and filter them by different criteria.
![contracts](https://github.com/Chameleeon/DTRealestate/blob/main/Screenshots/contracts.png)

Clicking the **Details** button on a contract allows the agent to view details of the contract.
![contract_details](https://github.com/Chameleeon/DTRealestate/blob/main/Screenshots/contract_details.png)

Clicking the **Save as PDF** button on a contract allows the agent to select the location and the name of the document, and then converts the existing contract to a PDF file.

### Inquiries

The inquiries page allows agents to view client inquiries about existing offers. It allows the agents to delete or reply to these inquiries, as well as to see details of the offer for which the inquiry has been sent.
![inquiries](https://github.com/Chameleeon/DTRealestate/blob/main/Screenshots/inquiries.png)

Clicking the **Info** button in the top right will take the agent to the **Offers** page and show the exact offer which the inquiry was sent for.

Clicking the **Delete** button deletes the inquiry, asking for confirmation first.

Clicking the **Reply** button next to the **Delete** button opens a pop up window in which the agent can send a response message to the client.
![reply](https://github.com/Chameleeon/DTRealestate/blob/main/Screenshots/reply.png)

### Offers

The Offers page allows agents to view existing offers and their details, edit them and add contracts for each offer. The agent can search and filter the offers.
![offers](https://github.com/Chameleeon/DTRealestate/blob/main/Screenshots/offers.png)

Clicking the **Edit** button in the top right opens up a pop up which allows the agent to change information of the existing offer.
![edit_offer](https://github.com/Chameleeon/DTRealestate/blob/main/Screenshots/edit_offer.png)

Clicking the **Info** btton inthe bottom right corner of the offer card opens a pop up window showing the detailed information of the selected offer.</br>
![info_offers](https://github.com/Chameleeon/DTRealestate/blob/main/Screenshots/offer_details.png)

Clicking the **Add** button next to the **Info** button opens a pop up window for adding a new contract for the selected offer.
![add_contract](https://github.com/Chameleeon/DTRealestate/blob/main/Screenshots/create_contract.png)

### Log Out

Clicking the **Log Out** button in the bottom left corner of the navigation menu, the user is logged out and taken back to the **Log In** page.
