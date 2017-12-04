//
//  Ship.swift
//  BattleShip_v1
//
//  Created by user128335 on 9/8/17.
//  Copyright © 2017 csu. All rights reserved.
//

import Cocoa


class Ship {
    var type: String
    var health = 0
    var owner = ""
    
    init(shipType: String){
        type = shipType
        if (type == "carrier") {
            health = 5
        }else if (type == "battleship"){
            health = 4
        }else if (type == "cruiser") || (type == "submarine"){
            health = 3
        }else if (type == "destroyer"){
            health = 2
        }
    }
    
    func setOwner(ownerName: String){
        owner = ownerName
    }
    
    func getOwner() -> String{
        return owner
    }
    
    func getHealth() -> Int{
        return health
    }
}
