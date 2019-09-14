import React, { Component } from "react";
import { connect } from 'react-redux';
import { RouteComponentProps } from "react-router-dom";

import { AppState } from "../redux/store";
import { getEntry, EntryState } from "../redux/entry";
import { VirtueLinkList } from "../components/virtue-link-list";

/*
export interface Entry {
  entryId: number,
  title: string,
  description: string,
  createdAt: Date,
  lastEdited: Date,
  starred: boolean,
  virtueLinks?: VirtueLink[]
}
*/

interface RouteProps {
  entryId: string
}

interface Props extends RouteComponentProps<RouteProps> {
  entry: EntryState,
  getEntry: (entryId: number) => any
}

class _EntryDetailPage extends Component<Props> {
  componentDidMount() {
    const entryId = Number.parseInt(this.props.match.params.entryId);
    this.props.getEntry(entryId);
  }

  handleEdit = (event: React.MouseEvent, entryId: number) => {
    this.props.history.push(`/entries/${entryId}/edit`);
  }

  render() {
    const currentEntry = this.props.entry.currentEntry;
    if (!currentEntry) return <div>Loading...</div>

    const { entryId, title, starred, description, createdAt, lastEdited, virtueLinks } = currentEntry;

    return (
      <div className="c-entry-detail">
        <button className="c-entry-detail__edit" onClick={(e) => this.handleEdit(e, entryId)}>Edit Entry</button>
        <h1>{title}</h1>
        <p>Starred: {starred ? "true" : "false"}</p>
        <p>{description}</p>
        <hr/>
        <p>Created: {createdAt}</p>
        <p>Last Edited: {lastEdited || "never"}</p>
        {virtueLinks && <VirtueLinkList virtueLinks={virtueLinks} />
        }
      </div>
    )
  }
}

const mapStateToProps = (state: AppState) => ({
  entry: state.entry
});

export const EntryDetailPage = connect(
  mapStateToProps, 
  { getEntry }
)(_EntryDetailPage);