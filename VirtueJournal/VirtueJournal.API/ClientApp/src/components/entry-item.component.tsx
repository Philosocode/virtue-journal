import React from "react";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faPencilAlt, faTrash } from "@fortawesome/free-solid-svg-icons";

import { VirtueLink } from "../redux/entry";

interface Props {
  key: number,
  createdAt: Date,
  entryId: number,
  lastEdited: Date,
  title: string,
  description: string,
  starred: boolean,
  virtueLinks?: VirtueLink[],
  handleEdit: (event: React.MouseEvent, entryId: number) => void,
  handleDelete: (event: React.MouseEvent, entryId: number) => void,
  handleClick: (entryId: number) => void
}

export const EntryItem = (props: Props): JSX.Element => {
  const { createdAt, entryId, lastEdited, title, description, starred, virtueLinks, 
    handleClick, handleDelete, handleEdit } = props;

  return (
    <div className="c-entry-item" onClick={() => handleClick(entryId)} >
      <div className="c-entry-item__top-row">
        <h3 className="c-entry-item__heading">{title}</h3>

        <div className="c-entry-item__icons">
          <div className="c-entry-item__edit" onClick={(e: React.MouseEvent) => handleEdit(e, entryId)}>
            <FontAwesomeIcon icon={faPencilAlt} />
          </div>
          <div className="c-entry-item__delete" onClick={(e: React.MouseEvent) => handleDelete(e, entryId)}>
            <FontAwesomeIcon icon={faTrash} />
          </div>
      </div>
      </div>

      <p>{description}</p>
      <p>Starred: {starred ? "✅" : "❌"}</p>
      <p>Created: {createdAt}</p>
      <p>Last Edited: {lastEdited ? lastEdited : "never"}</p>
      {
        virtueLinks && virtueLinks.length > 0 
          ? virtueLinks.map(vl => <div key={entryId}>ID: {vl.virtueId} | Difficulty: {vl.difficulty}</div>)
          : "Uncategorized"
      }
    </div>
  )
}