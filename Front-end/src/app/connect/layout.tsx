import Container from "@/components/common/Container"
import type { Metadata } from "next"

export const metadata: Metadata = {
    title: 'Connect Wallet | NFT Marketplace',
    description: 'Connect an Ethereum wallet with one of our Providers',
  }
  

export default function Layout({
  children,
}: {
  children: React.ReactNode
}) {
  return (
    <Container>
        {children}
    </Container>
  )
}