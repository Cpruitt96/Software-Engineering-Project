//
//  Player.swift
//  BattleShip_v1
//
//  Created by user128335 on 9/8/17.
//  Copyright © 2017 csu. All rights reserved.
//

import Cocoa


class Player {
    var numShips = 5
    var ships = [Ship]()
    var name: String
    
    init(playerShips: [Ship], playerName: String){
        ships.append(playerShips[0])
        ships.append(playerShips[1])
        ships.append(playerShips[2])
        ships.append(playerShips[3])
        ships.append(playerShips[4])
        name = playerName
        
        setShipOwner()
    }
    
    func getNumShips() ->Int{
        return numShips
    }
    
    func setShipOwner(){
        for i in ships {
            i.owner = name
        }
    }
}
