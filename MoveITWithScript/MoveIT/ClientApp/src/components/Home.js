import React, { Component } from 'react';

export class Home extends Component {
  static displayName = Home.name;

  render () {
    return (
      <div>
        <h1>Hello, world!</h1>
        <p>Welcome to your your best friends in relocation. We are here to help you</p>


            <div class="jumbotron">
                <h1 class="display-4">Move IT!</h1>
                <p class="lead"></p>
                <hr class="my-4"/>
                    <p>It uses utility classes for typography and spacing to space content out within the larger container.</p>
                    <p class="lead">
                        <a class="btn btn-primary btn-lg" href="#" role="button">Learn more</a>
                    </p>
            </div>

      </div>
    );
  }
}
