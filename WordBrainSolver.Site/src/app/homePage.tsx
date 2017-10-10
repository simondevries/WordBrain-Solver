import * as React from 'react';
import './home.less';
import '../../images/background.jpg';

import { GridInputPage } from './states/gridInputPage';
import { GameResultsPage } from './states/gameResultsPage';
import { LoadingPage } from './states/loadingPage';

interface IHomePageProps { };

interface IHomePageState {
  errorMessage,
  foundWords,
  showResult,
  isLoading
};

export class HomePage extends React.Component<IHomePageProps, IHomePageState> {
  constructor() {
    super()
    this.state = {
      errorMessage: '',
      foundWords: '',
      showResult: false,
      isLoading: false
    }

    this.setErrorMessage = this.setErrorMessage.bind(this);
    this.setIsLoading = this.setIsLoading.bind(this);
    this.setShowResults = this.setShowResults.bind(this);
    this.reset = this.reset.bind(this);
  }

  render() {
    return (
      <div className='container'>
        <h1>Word brain puzzle solver</h1>
        <div className={!this.state.showResult && !this.state.isLoading ? 'page' : 'hidden'}>
          <GridInputPage setErrorMessageFunction={this.setErrorMessage} setShowResultsFunction={this.setShowResults} setIsLoadingFunction={this.setIsLoading} />
        </div>
        <div className={this.state.showResult && !this.state.isLoading ? 'page' : 'hidden'}>
          <GameResultsPage errorMessage={this.state.errorMessage} foundWords={this.state.foundWords} resetFunction={this.reset} />
        </div>
        <div className={this.state.isLoading ? 'loading page' : 'hidden loading'}>
          <LoadingPage />
        </div>
      </div>
    );
  }

  setIsLoading(value) {
    this.setState({ isLoading: value });
  }

  setShowResults(value) {
    this.setState({ showResult: value });
  }

  setErrorMessage(value) {
    this.setState({ errorMessage: value })
  }

  reset() {
    this.setState({ showResult: false });
  }
}