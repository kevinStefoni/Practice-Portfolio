import React, { Component } from 'react';
import { Prism as SyntaxHighlighter } from 'react-syntax-highlighter';
import { coldarkDark as codeTheme } from 'react-syntax-highlighter/dist/esm/styles/prism';
import 'prismjs';
import 'prismjs/components/prism-csharp';
import './CodeSnippet.css';

export class CodeSnippet extends Component {
    render() {
        const { codeSnippet } = this.props;

        return (
            <div className="code-snippet">
                <SyntaxHighlighter language="csharp" style={codeTheme} className="language-csharp">
                    {codeSnippet}
                </SyntaxHighlighter>
            </div>
        );
    }
}
