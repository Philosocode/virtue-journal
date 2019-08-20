import React, { Component } from 'react'
import { connect } from 'react-redux';

import { AppState } from '../redux/store';
import { VirtueState, getVirtues } from "../redux/virtue";
import { Virtue } from "../components/virtue";
import { LinkButton } from "../components/shared/link-button";

interface VirtuesPageProps {
  virtueState: VirtueState,
  getVirtues: Function
}

class _VirtuesPage extends Component<VirtuesPageProps> {
  componentDidMount() {
    this.props.getVirtues();
  }

  getVirtueList = () => {
    const { virtues } = this.props.virtueState;
    let contentToRender;

    if (virtues.length > 0) {
      contentToRender = (
        virtues.map(v => (
          <Virtue 
            key={v.virtueId}
            color={v.color} 
            createdAt={v.createdAt} 
            description={v.description} 
            icon={v.icon} 
            name={v.name} 
            virtueId={v.virtueId}
          />
        )
      ))
    } else {
      contentToRender = <p>No virtues to display... why don't you add one?</p>
    }

    return contentToRender;
  }

  render() {
    return (
      <div>
        <h1>Virtues Page</h1>
        <LinkButton
          to="/virtues/create"
          className="c-virtue__create-button"
        >Create Virtue</LinkButton>
        { this.getVirtueList() }
      </div>
    )
  }
}

const mapStateToProps = (state: AppState) => ({
  virtueState: state.virtue
});

export const VirtuesPage = connect(
  mapStateToProps,
  { getVirtues }
)(_VirtuesPage);