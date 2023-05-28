import React, { Component } from 'react';
import CardGrid from './CardGrid';
import Card from './Card';

export class Home extends Component {
  static displayName = Home.name;

  constructor(props) {
    super(props);
    this.state = { grids: [], gameState: {}, loading: true };
  }

  componentDidMount() {
    this.getGame();
  }

  static renderGameGrids(grids) {
    <CardGrid>
    { Array(grids.length).fill().map((v, i) => (
        <Card key={i}>
          <h2>{v.id} Card {i + 1}</h2>
        </Card>
      ))
    }
  </CardGrid>
  }

  render() {
    let contents = this.state.loading
    ? <p><em>Loading...</em></p>
    : Home.render(this.state.grids);

    return (
      <div>
        <h1>ColourMemoryGame2</h1>
        <p>Good Luck Chuck.</p>
        {contents}
      </div>
    );
  }

  async getGame() {
    const response = await fetch("https://localhost:7146/game", {
      mode: "no-cors", // no-cors, *cors, same-origin
      headers: {
        "Content-Type": "application/json; charset=utf-8",
        // 'Content-Type': 'application/x-www-form-urlencoded',
      },
      cache: "default",
      credentials: "same-origin", // include, *same-origin, omit
      referrerPolicy: "no-referrer", // no-referrer, *no-referrer-when-downgrade, origin, origin-when-cross-origin, same-origin, strict-origin, strict-origin-when-cross-origin, unsafe-url
      //body: JSON.stringify(data), // body data type must match "Content-Type" header
    });
    console.log(response);
    const data = await response.json();
    this.setState({ grids: data.grids, gameState: data.currentStateOfGame, loading: false });
  }
}
