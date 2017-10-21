import * as React from 'react';

interface IGameResultsPageProps {
    errorMessage;
    foundWords;
    resetFunction;
};

export class GameResultsPage extends React.Component<IGameResultsPageProps> {
    render() {
        return (
            <div>
                <h3 className='error-message-content'>{this.props.errorMessage}</h3>
                <ul>
                    {
                        this.props.foundWords.map((res, index) => (
                            <div className='result-label'>{res}</div>
                        ))
                    }
                </ul>
                <button onClick={(event) => this.props.resetFunction()} className='my-button'>Back</button>
            </div>
        );
    }
}
