import {themes as prismThemes} from 'prism-react-renderer';
import type {Config} from '@docusaurus/types';
import type * as Preset from '@docusaurus/preset-classic';
import remarkMath from "remark-math";
import rehypeKatex from "rehype-katex";

async function fetchLatestNugetVersion(packageId: string, fallback: string): Promise<string> {
  try {
    const res = await fetch(`https://api.nuget.org/v3-flatcontainer/${packageId.toLowerCase()}/index.json`);
    const { versions } = await res.json() as { versions: string[] };
    return versions.at(-1) ?? fallback;
  } catch {
    return fallback;
  }
}

export default async function createConfig(): Promise<Config> {
  const toolkitVersion = await fetchLatestNugetVersion('McdaToolkit', '5.0.0-beta1');

  return {
    markdown: {
      mermaid: true,
    },
    title: 'Mcda Toolkit',
    tagline: 'McdaToolkit',
    url: 'https://your-docusaurus-site.example.com',
    baseUrl: '/mcda-toolkit-docs/',
    organizationName: 'SarcasticMoose',
    projectName: 'McdaToolkit',

    onBrokenLinks: 'warn',
    onBrokenMarkdownLinks: 'warn',

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
          path: 'api',
          routeBasePath: 'api',
          sidebarPath: require.resolve('./sidebar-api.js'),
        },
      ],
      require.resolve('docusaurus-lunr-search')
    ],

    stylesheets: [
      {
        href: 'https://cdn.jsdelivr.net/npm/katex@0.13.24/dist/katex.min.css',
        type: 'text/css',
        integrity: 'sha384-odtC+0UGzzFL/6PNoE8rX/SPcQDXBJ+uRepguP4QkPCm2LBxH3FA3y+fKSiJ+AmM',
        crossorigin: 'anonymous',
      },
    ],

    themeConfig: {
      mermaid: {
        theme: { light: 'dark', dark: 'dark' },
        options: {
          themeVariables: {
            background: 'transparent',
            primaryColor: '#1a2a25',
            primaryTextColor: 'rgba(255,255,255,0.82)',
            primaryBorderColor: 'rgba(37,194,160,0.4)',
            lineColor: 'rgba(255,255,255,0.25)',
            secondaryColor: '#0f1621',
            tertiaryColor: '#0d1117',
            clusterBkg: 'rgba(255,255,255,0.03)',
            clusterBorder: 'rgba(255,255,255,0.08)',
            edgeLabelBackground: '#0b0f14',
            fontFamily: 'inherit',
          },
        },
      },
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
          {
            type: 'docSidebar',
            sidebarId: 'apiSidebar',
            docsPluginId: 'api',
            position: 'left',
            label: 'API',
          }
        ],
      },
      footer: {
        style: 'dark',
        links: [
          {
            title: 'Docs',
            items: [
              { label: 'Get started', to: '/docs/category/get-started' },
              { label: 'Usage', to: '/docs/category/usage' },
            ],
          },
          {
            title: 'More',
            items: [
              { label: 'GitHub', href: 'https://github.com/SarcasticMoose/mcda-toolkit' },
              { label: 'Nuget', href: 'https://www.nuget.org/packages/McdaToolkit' },
            ],
          },
          {
            title: 'Contact',
            items: [
              { label: 'LinkedIn', href: 'https://www.linkedin.com/in/jakub-tokarczyk/' },
              { label: 'Email me', href: 'mailto:jakub.tokarczyk00@outlook.com' },
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
    customFields: {
      toolkitVersion,
      dotnetVersion: '8',
    },
    themes: ['@docusaurus/theme-mermaid'],
  };
}
