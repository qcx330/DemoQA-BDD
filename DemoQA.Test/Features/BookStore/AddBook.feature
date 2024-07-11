Feature: Add book
    User want to add book to their collection

  Background:
    Given User logged in the DemoQA

  @AddBook @Success
  Scenario: Add a book to the user's collection
    Given User navigates to the book store page
    When User selects a book
      | book             |
      | Git Pocket Guide |
    And User clicks on Add To Your Collection
    Then an alert "Book added to your collection." is shown
    And the book is shown in your profile
