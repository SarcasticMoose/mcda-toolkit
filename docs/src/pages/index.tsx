import type { ReactNode } from 'react';
import clsx from 'clsx';
import Link from '@docusaurus/Link';
import useDocusaurusContext from '@docusaurus/useDocusaurusContext';
import Layout from '@theme/Layout';
import HomepageFeatures from '@site/src/components/HomepageFeatures';
import Heading from '@theme/Heading';
import { useNugetVersion } from '@site/src/hooks/useNugetVersion';
import { useNugetDownloads } from '@site/src/hooks/useNugetDownloads';
import { useGitHubStars } from '@site/src/hooks/useGitHubStars';

import styles from './index.module.css';

const GitHubIcon = () => (
  <svg viewBox="0 0 24 24" width="16" height="16" fill="currentColor" style={{verticalAlign: 'middle', marginRight: 6}}>
    <path d="M12 0C5.37 0 0 5.37 0 12c0 5.31 3.435 9.795 8.205 11.385.6.105.825-.255.825-.57 0-.285-.015-1.23-.015-2.235-3.015.555-3.795-.735-4.035-1.41-.135-.345-.72-1.41-1.23-1.695-.42-.225-1.02-.78-.015-.795.945-.015 1.62.87 1.845 1.23 1.08 1.815 2.805 1.305 3.495.99.105-.78.42-1.305.765-1.605-2.67-.3-5.46-1.335-5.46-5.925 0-1.305.465-2.385 1.23-3.225-.12-.3-.54-1.53.12-3.18 0 0 1.005-.315 3.3 1.23.96-.27 1.98-.405 3-.405s2.04.135 3 .405c2.295-1.56 3.3-1.23 3.3-1.23.66 1.65.24 2.88.12 3.18.765.84 1.23 1.905 1.23 3.225 0 4.605-2.805 5.625-5.475 5.925.435.375.81 1.095.81 2.22 0 1.605-.015 2.895-.015 3.3 0 .315.225.69.825.57A12.02 12.02 0 0 0 24 12c0-6.63-5.37-12-12-12z"/>
  </svg>
);

const StarIcon = () => (
  <svg viewBox="0 0 24 24" width="13" height="13" fill="currentColor">
    <path d="M12 2l3.09 6.26L22 9.27l-5 4.87 1.18 6.88L12 17.77l-6.18 3.25L7 14.14 2 9.27l6.91-1.01L12 2z"/>
  </svg>
);

function HomepageHeader() {
  const version = useNugetVersion('McdaToolkit');
  const downloads = useNugetDownloads('McdaToolkit');
  const stars = useGitHubStars('SarcasticMoose/mcda-toolkit');

  return (
    <header className={clsx('hero', styles.heroBanner)}>
      <div className="container" style={{position: 'relative', zIndex: 1}}>

        <Heading as="h1" className={styles.title}>
          MCDA Toolkit
        </Heading>

        <div className={styles.badges}>
          <span className={clsx(styles.badge, styles.badgeVersion)}>v{version}</span>
          {downloads && <span className={clsx(styles.badge, styles.badgeDownloads)}>⬇ {downloads} downloads</span>}
          {stars && <span className={clsx(styles.badge, styles.badgeStars)}><StarIcon /> {stars}</span>}
          <span className={clsx(styles.badge, styles.badgeMit)}>MIT</span>
        </div>

        <p className={styles.subtitle}>
          Build multi-criteria decision systems with clarity and performance
        </p>

        <div className={styles.buttons}>
          <Link
            className="button button--primary button--lg"
            to="/docs/intro">
            Get Started
          </Link>

          <Link
            className={clsx('button button--outline button--lg', styles.githubBtn)}
            href="https://github.com/SarcasticMoose/mcda-toolkit">
            <GitHubIcon />GitHub
          </Link>
        </div>

        <p className={styles.note}>
          Designed for researchers, engineers, and decision-makers
        </p>

      </div>

      <div className={styles.scrollIndicator}>
        <span />
      </div>
    </header>
  );
}

export default function Home(): ReactNode {
  const { siteConfig } = useDocusaurusContext();
  return (
    <Layout
      title={siteConfig.title}
      description="Multi-criteria decision analysis toolkit for .NET">
      <HomepageHeader />
      <main>
        <HomepageFeatures />
      </main>
    </Layout>
  );
}
