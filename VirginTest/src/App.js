import React, { Component, Fragment } from 'react';
import './App.css';

export default class App extends Component {
    static displayName = App.name;

    constructor(props) {
        super(props);
        this.state = { scenarios: [], loading: true };
    }

    componentDidMount() {
        this.populateScenarioData();
    }

    static renderScenariosTable(scenarios) {
        return (
            <div className="container">
                <div className="row">
                <div className="col-xs-12">
                    <div className="table-responsive" data-pattern="priority-columns">
                        <table className='table table-bordered table-hover' aria-labelledby="tabelLabel">
                            <caption className="text-center">Scenarios summary</caption>
                            <thead>
                                <tr>
                                    <th>UserID</th>
                                    <th>Surname</th>
                                    <th>Forename</th>
                                    <th>SampleDate</th>
                                    <th>CreationDate</th>
                                    <th>NumMonths</th>
                                    <th>MarketID</th>
                                    <th>NetworkLayerID</th>
                                </tr>
                            </thead>
                            <tbody>
                                {scenarios.map(scenario =>
                                    <tr key={scenario.scenarioID}>
                                        <td>{scenario.userID}</td>
                                        <td>{scenario.surname}</td>
                                        <td>{scenario.forename}</td>
                                        <td>{new Date(scenario.sampleDate).toDateString()}</td>
                                        <td>{new Date(scenario.creationDate).toDateString()}</td>
                                        <td>{scenario.numMonths}</td>
                                        <td>{scenario.marketID}</td>
                                        <td>{scenario.networkLayerID}</td>
                                    </tr>
                                )}
                            </tbody>
                        </table>
                    </div>
                    </div>
                </div>
            </div>
        );
    }

    render() {
        let contents = this.state.loading
            ? <div className="container"><p><em>Loading... The server is starting. Please, refresh this page after 10 seconds if the data has not been displayed.</em></p></div>
            : App.renderScenariosTable(this.state.scenarios);

        return (
            <Fragment>
                <div className="virgin-header">
                    <h1>Virgin Media Test</h1>
                    <p>Results for the Virgin Media test by Jorge Sanchez.</p>
                </div>
                <div className="container virgin-body">
                    <h2>Summary of scenarios</h2>
                    {contents}
                </div>
            </Fragment>
        );
    }

    async populateScenarioData() {
        const response = await fetch('scenario');
        const data = await response.json();

        this.setState({ scenarios: data, loading: false });
    }
}
