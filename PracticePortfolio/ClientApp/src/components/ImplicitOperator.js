import React, { Component } from 'react';
import { CodeSnippet } from './CodeSnippet';

export class ImplicitOperator extends Component {
    constructor(props) {
        super(props);
        this.state = {
            demoCode: '',
            codeSnippet: '',
            codeSnippet2: '',
            result: '',
        };
    }

    componentDidMount() {
        this.fetchCodeSnippet();
        this.fetchResult();
    }

    fetchCodeSnippet = async () => {
        try {
            let response = await fetch('/ImplicitOperatorDemo.txt');
            let codeContent = await response.text();
            this.setState({ demoCode: codeContent });
        }
        catch {
            this.setState({ demoCode: 'Unable to load code.' });
        }

        try {
            let response = await fetch('/User.txt');
            let codeContent = await response.text();
            this.setState({ codeSnippet: codeContent });
        } catch {
            this.setState({ codeSnippet: 'Unable to load code.' });
        }

        try {
            let response = await fetch('/UserDTO.txt');
            let codeContent = await response.text();
            this.setState({ codeSnippet2: codeContent });
        } catch {
            this.setState({ codeSnippet2: 'Unable to load code.' });
        }
    };

    fetchResult = async () => {
        try {
            let response = await fetch(`/Csharp/implicit-operator`);
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
        const { codeSnippet2 } = this.state;
        const { demoCode } = this.state;
        const { result } = this.state;

        return (
            <div>
                <CodeSnippet codeSnippet={demoCode} />
                {result && (
                    <div>
                        <p>User</p>
                        <hr />
                        <p>Name: { result.name }</p>
                        <p>Email: { result.email }</p>
                    </div>
                )}
                <CodeSnippet codeSnippet={codeSnippet} />
                <CodeSnippet codeSnippet={codeSnippet2} />
            </div>
        );



    }
}
