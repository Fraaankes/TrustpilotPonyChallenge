import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import './Home.css'

export class Home extends Component {
  static displayName = Home.name;

  render() {
    return (
      <div>
        <h1 className="text-center">Welcome to the pony game !</h1>
        <Link to="/player" className="center">
          <button className="btn btn-success">Start new game</button>
        </Link>
      </div>
    );
  }
}
