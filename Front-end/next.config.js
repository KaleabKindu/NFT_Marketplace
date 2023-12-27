/** @type {import('next').NextConfig} */
const nextConfig = {
    webpack: (config) => {
        config.externals.push("pino-pretty", "encoding");
        return config;
      },
}

module.exports = nextConfig
