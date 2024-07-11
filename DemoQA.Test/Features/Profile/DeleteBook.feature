Feature: Delete book
    User want to delete book from User's collection

  Background:
    Given User already added book
      | Id            | Title            |
      | 9781449325862 | Git Pocket Guide |
    And User logged in the DemoQA

  @DeleteBook @Success
  Scenario Outline: Delete book from collection successfully
    Given User navigate to the profile page
    When User search for added book
    And User clicks on Delete icon
    And User clicks on OK button
    And User clicks on OK button of alert “Book deleted.”
    Then the book is not shown
