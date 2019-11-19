import React, { Component } from "react";
import { RouteComponentProps, withRouter } from 'react-router-dom';
import { connect } from 'react-redux';

import { AppState } from "../redux/store";
import { Entry, getAllEntries, getEntriesForVirtue, getUncategorizedEntries, deleteEntry } from "../redux/entry";

import { EntryItem } from "../components/entry-item.component";
import { LinkButton } from "../components/shared/link-button.component";

interface Props extends RouteComponentProps {
  entries: Entry[],
  getAllEntries: () => Promise<void>,
  getEntriesForVirtue: (virtueId: number) => Promise<void>,
  getUncategorizedEntries: () => Promise<void>,
  deleteEntry: (entryId: number) => Promise<undefined>
}

interface RouteProps {
  virtueId?: string
}

class _EntriesPage extends Component<RouteComponentProps<RouteProps> & Props> {
  componentDidMount() {
    const matchObj = this.props.match;
    const virtueIdParam = matchObj.params.virtueId;

    if (virtueIdParam) {
      let virtueId = Number.parseInt(virtueIdParam);
      this.setState({ virtueId: virtueIdParam });
      return this.props.getEntriesForVirtue(virtueId)
    }
    if (matchObj.path.includes("all")) {
      return this.props.getAllEntries();
    }

    if (matchObj.path.includes("uncategorized")) {
      return this.props.getUncategorizedEntries();
    } 
  }

  handleClick = (entryId: number) => {
    this.props.history.push(`/entries/${entryId}`);
  }

  handleEdit = (event: React.MouseEvent, entryId: number) => {
    event.stopPropagation();
    this.props.history.push(`/entries/${entryId}/edit`);
  }

  handleDelete = (event: React.MouseEvent, entryId: number) => {
    event.stopPropagation();
    this.props.deleteEntry(entryId);
  }

  getEntriesList = () => {
    const { entries } = this.props;
    let contentToRender;

    if (entries.length > 0) {
      contentToRender = (
        entries.map(e => (
          <EntryItem
            key={e.entryId}
            entryId={e.entryId}
            createdAt={e.createdAt}
            lastEdited={e.lastEdited}
            title={e.title}
            description={e.description}
            starred={e.starred}
            virtueLinks={e.virtueLinks}
            handleClick={this.handleClick}
            handleDelete={this.handleDelete}
            handleEdit={this.handleEdit}
          />
        )
      ))
    } else {
      contentToRender = <p>No entries to display... why don't you add one?</p>
    }

    return contentToRender;
  }

  render() {
    return (
      <div>
        <h1>Entries Page</h1>
        <LinkButton
          to="/entries/create"
          className="c-virtue__create-button"
        >Create Entry</LinkButton>
        { this.getEntriesList() }
      </div>
    )
  }
}

const mapStateToProps = (state: AppState) => ({
  entries: state.entry.entries
});

export const EntriesPage = withRouter(connect(
  mapStateToProps,
  { getAllEntries, getEntriesForVirtue, getUncategorizedEntries, deleteEntry }
)(_EntriesPage));