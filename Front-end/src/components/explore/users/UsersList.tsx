import { Creator } from '@/components/landing-page/TopCreators'

type Props = {}

const UsersList = (props: Props) => {
  return (
    <div className="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 lg:grid-cols-4 items-center gap-5">
        {
            Array.from({length:12}).map((_, index) => 
                <Creator key={index} index={index} showRank={false}/>
            )
        }

    </div>
  )
}

export default UsersList