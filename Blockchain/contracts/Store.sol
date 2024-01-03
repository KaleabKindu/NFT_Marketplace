// SPDX-License-Identifier: UNLICENSED
pragma solidity ^0.8.20;

// Uncomment this line to use console.log
import "hardhat/console.sol";

contract Store {
    uint public data;

    // Event emitted when the value is set
    event ValueSet(uint indexed oldValue, uint indexed newValue);

    constructor(){
        data = 500000;
    }

    function setValue(uint newValue) public {
        uint oldValue = data;
        data = newValue;
        emit ValueSet(oldValue, newValue);
    }

    function getValue() public view returns(uint){
        return data;
    }
}