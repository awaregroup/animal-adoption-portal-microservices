import React, { Component } from 'react';
import { Collapse, Container, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink } from 'reactstrap';
import { Link } from 'react-router-dom';
import './NavMenu.css';

interface NavMenuProps {
    handleLogin: () => void
    handleLogout: () => void
    userName: string | undefined
}


interface NavMenuState {
    collapsed: boolean
}

export class NavMenu extends Component<NavMenuProps, NavMenuState> {

    constructor(props: any) {
        super(props);

        this.toggleNavbar = this.toggleNavbar.bind(this);
        this.state = {
            collapsed: true
        };
    }

    toggleNavbar() {
        this.setState({
            collapsed: !this.state.collapsed
        });
    }



    //TODO: change the localhost value to environment var
    render() {
 
        return (
            <header>
                <Navbar className="navbar-expand-sm navbar-toggleable-sm ng-white border-bottom box-shadow mb-3" light>
                    <Container>
                        <NavbarBrand tag={Link} to="/">Animal Adoption Portal</NavbarBrand>
                        {this.props.userName && <span>Welcome {this.props.userName}!</span>}
                        <NavbarToggler onClick={this.toggleNavbar} className="mr-2" />
                        <Collapse className="d-sm-inline-flex flex-sm-row-reverse" isOpen={!this.state.collapsed} navbar>
                            <ul className="navbar-nav flex-grow">
                                <NavItem>
                                    <NavLink tag={Link} className="text-dark" to="/">Home</NavLink>
                                </NavItem>
                                <NavItem>
                                    <NavLink tag={Link} className="text-dark" to="/cart">Cart</NavLink>
                                </NavItem>
                                <NavItem>
                                    {this.props.userName ?
                                        <NavLink tag={Link} className="text-dark" to="#" onClick={this.props.handleLogout}>Logout</NavLink> :
                                        <NavLink tag={Link} className="text-dark" to="#" onClick={this.props.handleLogin}>Login</NavLink>
                                    }
                                </NavItem>
                            </ul>
                        </Collapse>
                    </Container>
                </Navbar>
            </header>
        );
    }
}
