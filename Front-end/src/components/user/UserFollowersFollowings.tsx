import React from 'react'
import { Creator } from '../landing-page/TopCreators'

type Props = {}

const UserFollowersFollowings = (props: Props) => {
  return (
    <div className="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 lg:grid-cols-4 items-center gap-5">
        {
            Array.from({length:8}).map((_, index) => 
                <Creator key={index} index={index}/>
            )
        }

    </div>
  )
}

export default UserFollowersFollowings