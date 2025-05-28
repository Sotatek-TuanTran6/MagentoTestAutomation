Feature: Purchase Product from Magento Test Store

  Scenario: Registered user buys products and verifies order
    Given the user navigates to the Magento store
    And logs in with valid credentials
    When the user adds 2 Men's Jackets and 1 Men's Pants to the cart
    And proceeds to checkout
    And enters a valid delivery address and selects a delivery method
    And places the order
    Then the order should be listed under "My Orders"
