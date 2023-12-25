import { Avatar } from '@/components/common/Avatar'
import { TypographyH2, TypographyH3, TypographyP } from '@/components/common/Typography'
import { Button } from '@/components/ui/button'
import React from 'react'

type Props = {}

const Page = (props: Props) => {
  return (
    <div className='flex flex-col w-[60%] mx-auto mt-16'>
        <div className='pb-8 border-b'>
            <TypographyH2 text='Connect your Wallet'/>
            <TypographyP text='Connect with one of our available wallet providers or create a new one.'/>
        </div>
        <div className='flex flex-col gap-5 pt-8'>
            <Button variant={'secondary'} className='justify-start gap-10 h-16'>
                <Avatar className='h-16 w-16' src='/metamask-logo.svg' />
                <TypographyH3 text={'Metamask'}/>
            </Button>
            <Button variant={'secondary'} className='justify-start gap-10 h-16'>
                <Avatar className='h-16 w-16' src='/walletconnect-logo.svg' />
                <TypographyH3 text={'WalletConnect'}/>
            </Button>
            <Button variant={'secondary'} className='justify-start gap-10 h-16'>
                <Avatar className='h-16 w-16' src='/coinbase-wallet-logo.svg' />
                <TypographyH3 text={'Coinbase Wallet'}/>
            </Button>
        </div>
    </div>
  )
}

export default Page