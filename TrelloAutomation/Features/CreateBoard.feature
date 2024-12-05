@UI
Feature: Create Trello Board
    
    As a Trello user
    I want to create a new board
    So that I can organize my tasks

Background:
    Given the user is at the Home Page
    And the user logs in to Trello

Scenario: User creates a new board from scratch
    Given the user is on the Boards section
    When the user creates a new board named "Test Board"
    Then the user sees the board in the Trello Workspace
    And confirms that the board has the correct visibility

Scenario Outline: User creates a new board using different templates
    Given the user is on the Boards section
    When the user creates a new board selecting the template "<template>"
    Then the user sees the board in the Trello Workspace
    And confirms that the board has the correct visibility

  Examples:
      | template               |
      | Design Huddle          |
      | 1-on-1 Meeting Agenda  |
      | Company Overview       |