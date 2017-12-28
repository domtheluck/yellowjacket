Feature: Login
	Test the login functionnality

Scenario: Login Success
	Given I enter 'myusername' in 'username' textbox
	And I enter 'mypassword' in 'password' textbox
	When I click on 'login' button
	Then I see my profile page

Scenario: Login Invalid Password
	Given I enter 'myusername' in 'username' textbox
	When I click on 'login' button
	Then I see invalid password message