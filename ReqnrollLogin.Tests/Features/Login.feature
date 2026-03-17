@ui
Feature: Login
  As a user
  I want to log in
  So that I can access secured pages

  # TODO: Write login test scenarios here
  # 
  # Requirements:
  # 1. Test successful login with valid credentials
  #    - Username: tomsmith
  #    - Password: SuperSecretPassword!
  #    - Verify success message, secure page access, and page data
  #    - Include logout flow
  # 
  # 2. Test failed login with invalid credentials
  #    - Use invalid username/password
  #    - Verify error message is displayed

  # Test site: https://the-internet.herokuapp.com/login

  Scenario: Successful login with valid credentials
    Given I open the main page at "https://the-internet.herokuapp.com/login"
    When I login with valid credentials
    Then I should be on a page with header "Secure Area"
    And I should see a flash message containing "You logged into a secure area!"
    When I click the logout button
    Then I should be on a page with header "Login Page"

  Scenario: Failed login with invalid credentials
    Given I open the main page at "https://the-internet.herokuapp.com/login"
    When I login with invalid credentials
    Then I should see a flash message containing "Your username is invalid!"