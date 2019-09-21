import React, { Component } from 'react';
import { Container } from 'reactstrap';
import { NavMenu } from './NavMenu';

interface LayoutProps {
    handleLogin : () => void
    handleLogout: () => void
    userName : string | undefined
}

export class Layout extends Component<LayoutProps> {
  
  render () {
    return (
      <div>
        <NavMenu {...this.props} />
        <Container>
          {this.props.children}
        </Container>
      </div>
    );
  }
}
