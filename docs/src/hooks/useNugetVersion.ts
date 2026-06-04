import { useEffect, useState } from 'react';
import useDocusaurusContext from '@docusaurus/useDocusaurusContext';

export function useNugetVersion(packageId: string): string {
  const { siteConfig } = useDocusaurusContext();
  const fallback = siteConfig.customFields?.toolkitVersion as string ?? '';

  const [version, setVersion] = useState<string>(fallback);

  useEffect(() => {
    fetch(`https://api.nuget.org/v3-flatcontainer/${packageId.toLowerCase()}/index.json`)
      .then(res => res.json())
      .then(({ versions }: { versions: string[] }) => {
        const latest = versions.at(-1);
        if (latest) setVersion(latest);
      })
      .catch(() => {});
  }, [packageId]);

  return version;
}
