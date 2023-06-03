import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import './NavMenu.css';

export class NavMenu extends Component {
    constructor(props) {
        super(props);
        this.state = {
            activeLink: '',
            isClosing: false
        };
    }

    componentDidMount() {
        const { pathname } = window.location;
        this.setActiveLink(pathname);
        window.addEventListener('popstate', this.handlePopState);
    }

    componentWillUnmount() {
        window.removeEventListener('popstate', this.handlePopState);
    }

    handlePopState = () => {
        const { pathname } = window.location;
        this.setActiveLink(pathname);
    };

    setActiveLink = (pathname) => {
        this.setState({ activeLink: pathname });
    };

    handleCloseNav = () => {
        this.setState({ isClosing: true });
        this.props.onCloseNav();
        setTimeout(() => {
            this.setState({ isClosing: false });
        }, 250);
    };

    render() {
        const { isPushed } = this.props;
        const { activeLink, isClosing } = this.state;
        const showHamburger = !isPushed && !isClosing;

        return (
            <div>
                <div id="mySidenav" className={`sidenav ${isPushed ? 'open' : ''} ${isClosing ? 'closing' : ''}`}>
                    <button className="close-nav-btn" onClick={this.handleCloseNav}>
                        &times;
                    </button>
                    <Link
                        to="/"
                        className={`nav-text ${activeLink === '/' ? 'active-link' : ''}`}
                        onClick={() => this.setActiveLink('/')}
                    >
                        Home
                    </Link>
                    <Link
                        to="/singleton"
                        className={`nav-text ${activeLink === '/singleton' ? 'active-link' : ''}`}
                        onClick={() => this.setActiveLink('/singleton')}
                    >
                        Singleton
                    </Link>
                    <Link
                        to="/adapter"
                        className={`nav-text ${activeLink === '/adapter' ? 'active-link' : ''}`}
                        onClick={() => this.setActiveLink('/adapter')}
                    >
                        Adapter
                    </Link>
                    <Link
                        to="/explicit-operator"
                        className={`nav-text ${activeLink === '/explicit-operator' ? 'active-link' : ''}`}
                        onClick={() => this.setActiveLink('/explicit-operator')}
                    >
                        Explicit Operator
                    </Link>
                </div>

                {showHamburger && (
                    <button onClick={this.props.onPushToggle} className="hamburger-button">
                        <span className="hamburger-icon"></span>
                    </button>
                )}
            </div>
        );
    }
}

export default NavMenu;
