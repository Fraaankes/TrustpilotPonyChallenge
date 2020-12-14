import React, { Component } from 'react';
import { MazeService } from '../services/mazeService'
import './Game.css'

export class Game extends Component {

  static timer;

  constructor(props) {
    super(props);
    this.state = { maze: null, loading: true, playerName: "", hiddenUrl: null };
    this.mazeService = new MazeService();
  }

  // Component state hooks
  componentDidMount() {
    const { playerName } = this.props.location.state;
    this.setState({
      playerName: playerName
    });
    if (playerName) {
      this.createNewMaze(playerName);
      window.onkeydown = this.onKeyDown.bind(this);
      this.startGameLoop();
    }
  }

  componentWillUnmount() {
    if (Game.timer) {
      clearInterval(Game.timer);
    }
  }

  // Start a timer for Domo to "move" by himself
  async startGameLoop() {
    if (Game.timer) {
      clearInterval(Game.timer);
    }
    Game.timer = setInterval(async () => await this.move("stay"), 1000);
  }

  async onKeyDown(event) {
    const keyMap = {
      37: "w",
      38: "n",
      39: "e",
      40: "s"
    };
    if (keyMap[event.keyCode]) {
      await this.move(keyMap[event.keyCode]);
      await this.startGameLoop();
    }
  }

  async move(direction) {
    const stateData = await this.mazeService.move(this.state.maze.maze_id, direction);
    await this.updateGameState(stateData);
  }

  async updateGameState(stateData) {
    if (stateData.state && stateData.state !== "active") {
      clearInterval(Game.timer);
      this.setState({ hiddenUrl: stateData["hidden-url"] });
    } else {
      await this.updateMaze();
    }
  }

  getLocationClassName(maze, index) {
    if (index === maze.pony[0]) {
      return "player " + this.state.playerName.toLowerCase().replace(" ", "_");
    }
    if (index === maze.domokun[0]) {
      return "monster";
    }
    if (index === maze["end-point"][0]) {
      return "endpoint";
    }
    return "";
  }

  renderMaze(maze) {
    if (!maze.data) {
      return (
        <div>There was no data</div>
      );
    }
    const width = maze.size[0];

    // Split the 1D array into a 2D array for easier rendering
    let chunkedArray = [];
    for (let i = 0; i < maze.data.length; i += width) {
      chunkedArray.push(maze.data.slice(i, i + width));
    }

    let renderedTd = -1; // -1 to start the first cell at 0
    let renderedTr = -1;
    return (
      <table className="table">
        <tbody>
          {chunkedArray.map(chunks =>
            <tr key={++renderedTr}>
              {chunks.map(c =>
                <td key={++renderedTd} className={`cell ${c.join(' ')} ${this.getLocationClassName(maze, renderedTd)}`.trim()}></td>
              )}
            </tr>
          )}
        </tbody>
      </table>
    );
  }

  async createNewMaze(name) {
    const maze = await this.mazeService.createNewMaze(name);
    this.setState({ maze: maze, loading: false });
  }

  async updateMaze() {
    const maze = await this.mazeService.get(this.state.maze.maze_id)
    this.setState({ maze: maze });
  }

  render() {
    let contents =
      this.state.loading
        ? <p><em>Loading...</em></p>
        : this.renderMaze(this.state.maze);

    return (
      <div>
        {
          this.state.hiddenUrl
            ?
            <img className="game-result-img" src={this.state.hiddenUrl} alt=""></img>
            :
            <div>
              <div className="game-container">
                {contents}
              </div>
              <p>Use the arrows keys to move</p>
            </div>
        }
      </div>
    );
  }
}
