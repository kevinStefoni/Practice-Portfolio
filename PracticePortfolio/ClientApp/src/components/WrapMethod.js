import React, { Component } from 'react';
import { CodeSnippet } from './CodeSnippet';

export class WrapMethod extends Component {
    constructor(props) {
        super(props);
        this.state = {
            demoCode: '',
            codeSnippet: '',
            result: '',
            payRate: '18.00',
            dailyHoursWorked: [[8, 7, 6], [8, 8, 8]]
        };
    }

    componentDidMount() {
        this.fetchCodeSnippet();
        this.fetchResult();
    }

    fetchCodeSnippet = async () => {
        try {
            let response = await fetch('/WrapMethodDemo.txt');
            let codeContent = await response.text();
            this.setState({ demoCode: codeContent });
        }
        catch {
            this.setState({ demoCode: 'Unable to load code.' });
        }

        try {
            let response = await fetch('/Employee.txt');
            let codeContent = await response.text();
            this.setState({ codeSnippet: codeContent });
        } catch {
            this.setState({ codeSnippet: 'Unable to load code.' });
        }
    };

    fetchResult = async () => {
        try {
            const { payRate, dailyHoursWorked } = this.state;

            const response = await fetch('/TestingMethods/wrap-method-payload', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({ payRate, dailyHoursWorked }),
            });
            if (response.ok) {
                const result = await response.json();
                this.setState({ result: result });
            } else {
                this.setState({ result: null });
            }
        } catch {
            this.setState({ result: null });
        }
    };

    render() {
        const { codeSnippet } = this.state;
        const { demoCode } = this.state;
        const { result } = this.state;
        const { payRate } = this.state;
        const { dailyHoursWorked } = this.state;

        return (
            <div>
                <CodeSnippet codeSnippet={demoCode} />
                {result && (
                    <div>
                        <p>The hourly pay rate is: ${payRate}.</p>
                        <p>Hours worked:</p>
                        <ul>
                            {dailyHoursWorked.map((hours, index) => (
                                <li key={index}>
                                    {hours.map((hour, innerIndex) => (
                                        <span key={innerIndex}>
                                            {innerIndex > 0 && ", "}
                                            {hour}
                                        </span>
                                    ))}
                                </li>
                            ))}
                        </ul>
                        <p>The total pay is: ${result.toFixed(2)}.</p>
                    </div>
                )}
                <CodeSnippet codeSnippet={codeSnippet} />
            </div>
        );



    }
}
