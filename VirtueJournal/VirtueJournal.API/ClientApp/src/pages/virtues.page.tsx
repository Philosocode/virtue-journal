import React, { Component } from 'react'
import { connect } from 'react-redux';
import { withRouter, RouteComponentProps } from "react-router-dom";

import { AppState } from '../redux/store';
import { VirtueState, getVirtues, deleteVirtue, setCurrentVirtue } from "../redux/virtue";
import { Virtue } from "../components/virtue.component";
import { LinkButton } from "../components/shared/link-button.component";

interface VirtuesPageProps extends RouteComponentProps {
  virtueState: VirtueState,
  getVirtues: () => any,
  deleteVirtue: (virtueId: number) => any,
  setCurrentVirtue: Function
}

class _VirtuesPage extends Component<VirtuesPageProps> {
  componentDidMount() {
    this.props.getVirtues();
  }

  handleClick = (virtueId: number) => {
    const virtueToSet = this.props.virtueState.virtues.find(v => v.virtueId === virtueId);

    this.props.setCurrentVirtue(virtueToSet);
    this.props.history.push(`/virtues/${virtueId}/entries`);
  }

  handleEdit = (event: React.MouseEvent, virtueId: number) => {
    event.stopPropagation();
    this.props.history.push(`/virtues/${virtueId}/edit`);
  }

  handleDelete = (event: React.MouseEvent, virtueId: number) => {
    event.stopPropagation();
    this.props.deleteVirtue(virtueId);
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
            handleDelete={this.handleDelete}
            handleEdit={this.handleEdit}
            handleClick={this.handleClick}
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

export const VirtuesPage = withRouter(connect(
  mapStateToProps,
  { getVirtues, deleteVirtue, setCurrentVirtue }
)(_VirtuesPage));