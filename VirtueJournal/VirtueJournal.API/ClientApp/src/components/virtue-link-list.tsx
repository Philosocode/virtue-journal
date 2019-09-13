import React from "react";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faTrash } from "@fortawesome/free-solid-svg-icons";

import { VirtueLink } from "../redux/entry";

interface Props {
  virtueLinks: VirtueLink[],
  canDelete?: boolean,
  handleDelete?: (virtueId: number) => void
}

export const VirtueLinkList = (props: Props): JSX.Element => {
  return (
    <ul className="c-virtue-link__list">{props.virtueLinks.map(vl => (
      <li className="c-virtue-link__item" key={vl.virtueId}>
        {
          props.canDelete && (
            <div className="c-virtue-link__delete" onClick={() => props.handleDelete && props.handleDelete(vl.virtueId)}>
              <FontAwesomeIcon icon={faTrash} className="c-virtue-link__delete" />
            </div>
          )
        }
        <p>Virtue ID: {vl.virtueId}</p>
        <p>Difficulty: {vl.difficulty}</p>
      </li>
      ))}
    </ul>
  )
}