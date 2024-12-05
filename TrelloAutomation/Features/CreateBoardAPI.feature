@API
Feature: CreateBoardAPI

    As a Trello user
    I want to use the Create Board API
    So that I can programmatically organize my tasks

Background:
    Given I have a valid Trello API key and token


Scenario: Successfully create a new board
    When I create a new board with the title "Test Board API"
    Then the board should be created successfully

Scenario: Fail to create a board without a title
    When I try to create a new board without a title
    Then I should receive an error message "Board name cannot be empty (Parameter 'boardTitle')"