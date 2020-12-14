import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import './Player.css'

export class Player extends Component {
  static pathName = "/game";

  render() {
    return (
      <div>
        <h1 className="text-center">Please select your character</h1>

        <div className="d-flex p-2">
          <div className="center">
            <Link to={{
              pathname: Player.pathName,
              state: {
                playerName: "Rainbow Dash"
              }
            }}>
              <img className="player-select-img" src="/img/rainbow_dash.png" alt=""></img>
            </Link>
          </div>
          <div className="center">
            <Link to={{
              pathname: Player.pathName,
              state: {
                playerName: "Applejack"
              }
            }}>
              <img className="player-select-img" src="/img/applejack.png" alt=""></img>
            </Link>
          </div>
          <div className="center">
            <Link to={{
              pathname: Player.pathName,
              state: {
                playerName: "Pinkie Pie"
              }
            }}>
              <img className="player-select-img" src="/img/pinkie_pie.png" alt=""></img>
            </Link>
          </div>
        </div>
      </div>
    );
  }
}
