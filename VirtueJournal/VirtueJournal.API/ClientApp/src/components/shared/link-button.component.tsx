import React from 'react'
import PropTypes from 'prop-types'
import { withRouter } from 'react-router'

// https://stackoverflow.com/a/49439893
/*
<LinkButton
  to='/path/to/page'
  onClick={(event) => {
    console.log('custom event here!', event)
  }}
>Push My Buttons!</LinkButton>
*/

const _LinkButton = (props: any) => {
  const {
    history,
    location,
    match,
    staticContext,
    to,
    onClick,
    // ⬆ filtering out props that `button` doesn’t know what to do with.
    ...rest
  } = props
  return (
    <button
      {...rest} // `children` is just another prop!
      onClick={(event) => {
        onClick && onClick(event)
        history.push(to)
      }}
    />
  )
}

_LinkButton.propTypes = {
  to: PropTypes.string.isRequired,
  children: PropTypes.node.isRequired
}

export const LinkButton = withRouter(_LinkButton);