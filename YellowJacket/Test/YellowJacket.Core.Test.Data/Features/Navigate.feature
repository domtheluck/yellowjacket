Feature: Navigate
	Test the navigation

Scenario: Navigate Landing Page
	Given I enter 'http://www.mysite.com'
	Then I see the 'Landing' page

Scenario: Navigate Register Page
	Given I enter 'http://www.mysite.com/register'
	Then I see the 'Register' page

Scenario: Navigate Profile Page
	Given I enter 'http://www.mysite.com/profile'
	Then I see the 'Profile' page
