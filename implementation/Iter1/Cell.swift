//
//  Cell.swift
//  BattleShip_v1
//
//  Created by user128335 on 9/8/17.
//  Copyright © 2017 csu. All rights reserved.
//

import Cocoa

class Cell {
    var owner = ""
    var owned = false
    var shipIndex = 0
    var shipIsHidden = false
    
    init(){
        
    }
    
    func addPlayerShip(nameOfPlayer: String, shipInd: Int){
        owner = nameOfPlayer
        shipIndex = shipInd
        owned = true
    }
    
    func isOwned() -> Bool{
        return owned
    }
    
    func getOwner() ->String{
        return owner
    }
    
    func getShipIndex() ->Int{
        return shipIndex
    }
    
    
    func printCell(){
        if shipIsHidden == false{
            
            print("-----------")
            print("|         |")
            print("|         |")
            print("|  *****  |")
            print("|         |")
            print("|         |")
            print("-----------")
        }
    }
    
}
