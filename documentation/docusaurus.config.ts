import {themes as prismThemes} from 'prism-react-renderer';
import type {Config} from '@docusaurus/types';
import type * as Preset from '@docusaurus/preset-classic';
import remarkMath from "remark-math";
import rehypeKatex from "rehype-katex";

// This runs in Node.js - Don't use client-side code here (browser APIs, JSX...)

const config: Config = {
  title: 'Mcda Toolkit',
  tagline: 'McdaToolkit',
  url: 'https://your-docusaurus-site.example.com',
  baseUrl: '/mcda-toolkit-docs/',
  organizationName: 'SarcasticMoose', // Usually your GitHub org/user name.
  projectName: 'McdaToolkit', // Usually your repo name.

  onBrokenLinks: 'warn',
  onBrokenMarkdownLinks: 'warn',

  // Even if you don't use internationalization, you can use this field to set
  // useful metadata like html lang. For example, if your site is Chinese, you
  // may want to replace "en" with "zh-Hans".
  i18n: {
    defaultLocale: 'en',
    locales: ['en'],
  },

  presets: [
    [
      'classic',
      {
        docs: {
          sidebarPath: './sidebars.ts',
          remarkPlugins: [remarkMath],
          rehypePlugins: [rehypeKatex],
      },
        theme: {
          customCss: ['./src/css/custom.css','./src/css/custom-footer.css'],
        },
      } satisfies Preset.Options,
    ],
  ],
  plugins: [
    [
      '@docusaurus/plugin-content-docs',
      {
        id: 'api',
        path: 'api',                   // path to your custom folder
        routeBasePath: 'api',          // this is the URL path
        sidebarPath: require.resolve('./sidebar-api.js'), // optional sidebar file
      },
    ],
    require.resolve('docusaurus-lunr-search')
  ],

  stylesheets: [
    {
      href: 'https://cdn.jsdelivr.net/npm/katex@0.13.24/dist/katex.min.css',
      type: 'text/css',
      integrity:
          'sha384-odtC+0UGzzFL/6PNoE8rX/SPcQDXBJ+uRepguP4QkPCm2LBxH3FA3y+fKSiJ+AmM',
      crossorigin: 'anonymous',
    },
  ],

  themeConfig: {
    colorMode: {
      defaultMode: 'dark',
      disableSwitch: true,
      respectPrefersColorScheme: true,
    },
    image: 'img/docusaurus-social-card.jpg',
    navbar: {
      title: 'McdaToolkit',
      logo: {
        alt: 'My Site Logo',
        src: 'img/logo.png',
      },
      items: [
        {
          type: 'docSidebar',
          sidebarId: 'docsSidebar',
          position: 'left',
          label: 'Docs',
        },
        {to: 'api', label: 'Api', position: 'left'}
      ],
    },
    footer: {
      style: 'dark',
      links: [
        {
          title: 'Docs',
          items: [
            {
              label: 'Get started',
              to: '/docs/category/get-started',
            },
            {
              label: 'Usage',
              to: '/docs/category/usage',
            },
          ],
        },
        {
          title: 'More',
          items: [
            {
              label: 'GitHub',
              href: 'https://github.com/SarcasticMoose/mcda-toolkit',
            },
            {
              label: 'Nuget',
              href: 'https://www.nuget.org/packages/McdaToolkit',
            },
          ],
        },
        {
          title: 'Contact',
          items: [
            {
              label: "LinkedIn",
              href: 'https://www.linkedin.com/in/jakub-tokarczyk/',
            },
            {
              label: 'Email me',
              href: 'mailto:jakub.tokarczyk00@outlook.com',
            },
          ],
        },
      ],
      copyright: `Copyright © ${new Date().getFullYear()} McdaToolkit, Inc. Built with Docusaurus.`,
    },
    prism: {
      theme: prismThemes.github,
      darkTheme: prismThemes.dracula,
      additionalLanguages: ['csharp']
    },
  } satisfies Preset.ThemeConfig,
};

export default config;
